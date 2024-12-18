import React, { useEffect, useState, useContext } from "react";
import "./Review.css";
import { Context } from "../../App";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faComment, faThumbsUp, faThumbsDown } from "@fortawesome/free-solid-svg-icons";
import { ReviewsApi } from "../../API/ReviewsApi";


export default function Review({ review, onReviewChange }) {
    const { isAuthenticated, user } = useContext(Context);
    const [isLoading, setIsLoading] = useState(true);
    const [reaction, setReaction] = useState(null);
    const [likeDislikeLoading, setLikeDislikeLoading] = useState({ like: false, dislike: false });

    const fetchUserReaction = async () => {
        if (user) {
            try {
                const response = await ReviewsApi.getReactionByReviewUser(review.id, user.id);
                if (response.success) {
                    setReaction(response.reaction);
                }
            } catch (error) {
                console.error(error);
            }
        }
    };

    const addReaction = async (isLike) => {
        setLikeDislikeLoading({ like: true, dislike: true });
        try {
            const response = await ReviewsApi.submitReaction(review.id, isLike);
            if (response.success) {
                setReaction({ reviewId: review.id, userId: user.id, isLike });
            } else {
                alert(response.message);
            }
        } catch (error) {
            alert(error);
        } finally {
            setLikeDislikeLoading({ like: false, dislike: false });
        }
    };

    const removeReaction = async () => {
        if (!reaction) return;
        setLikeDislikeLoading({ like: true, dislike: true });
        try {
            const response = await ReviewsApi.removeReaction(reaction.id);
            if (response.success) {
                setReaction(null);
            } else {
                alert(response.message);
            }
        } catch (error) {
            alert(error);
        } finally {
            setLikeDislikeLoading({ like: false, dislike: false });
        }
    };

    const updateReaction = async () => {
        if (!reaction) return;
        setLikeDislikeLoading({ like: true, dislike: true });
        try {
            const response = await ReviewsApi.updateReaction(reaction.id);
            if(!response.success) {
                alert(response.message);
            }
        } catch (error) {
            alert(error);
        } finally {
            setLikeDislikeLoading({ like: false, dislike: false });
        }
    };

    const handleAddReaction = async (isLike) => {
        setIsLoading(true)
        if (reaction && reaction.isLike === isLike) {
            console.log("remove")
            await removeReaction();
        } else if (reaction && reaction.isLike !== isLike) {
            await updateReaction()
            console.log("update");
        } else {
            console.log("add")
            await addReaction(isLike);
        }
        onReviewChange()
        fetchUserReaction()
        setIsLoading(false)
    };

    useEffect(() => {
        setIsLoading(true)
        fetchUserReaction();
        console.log(review.likes)
        console.log(review.dislikes)
        setIsLoading(false);
    }, [user, review.id]);

    if (isLoading) {
        return <p>Loading...</p>;
    }

    return (
        <div className="review">
            <div className="review-info">
                <p>
                    <span className="review-info-prop-label">by</span>
                    <span className="review-info-prop">{review.username} •</span>
                    <span className="review-info-prop-label">Rating:</span>
                    <span className="review-info-prop">{review.rating}/10</span>
                </p>
                <p>
                    <span className="review-info-prop-label">Created at:</span>
                    <span className="review-info-prop">{new Date(review.createdAt).toLocaleString()} •</span>
                    <span className="review-info-prop-label">Last updated at:</span>
                    <span className="review-info-prop">{new Date(review.lastUpdatedAt).toLocaleString()}</span>
                </p>
            </div>

            {review.content && (
                <>
                    <div className="review-content">
                        <p>{review.content}</p>
                    </div>
                    <ReactionButtons
                        reaction={reaction}
                        likeDislikeLoading={likeDislikeLoading}
                        handleAddReaction={handleAddReaction}
                        review={review}
                        isAuthenticated={isAuthenticated}
                        fetchUserReaction={fetchUserReaction}
                        onReviewChange={onReviewChange}
                    />
                </>
            )}
        </div>
    );
}

function ReactionButtons({ reaction, likeDislikeLoading, handleAddReaction, review, isAuthenticated, fetchUserReaction, onReviewChange }) {
    return (
        <div className="review-footer">
            <ActionButton
                className="icon-button"
                icon={faComment}
                label="Comments"
                count={review.commentCount}
                disabled={review.commentCount === 0}
            />
            {isAuthenticated && (
                <>
                    <ActionButton
                        className={`like-btn ${reaction?.isLike ? "active" : ""} ${likeDislikeLoading.like ? "loading" : ""}`}
                        icon={faThumbsUp}
                        label="Like"
                        count={review.likes}
                        onClick={() => handleAddReaction(true)}
                    />
                    <ActionButton
                        className={`dislike-btn ${reaction?.isLike === false ? "active" : ""} ${likeDislikeLoading.dislike ? "loading" : ""}`}
                        icon={faThumbsDown}
                        label="Dislike"
                        count={review.dislikes}
                        onClick={() => handleAddReaction(false)}
                    />
                </>
            )}
        </div>
    );
}

function ActionButton({ className, icon, label, count, onClick, disabled }) {
    return (
        <button className={className} onClick={onClick} disabled={disabled}>
            <FontAwesomeIcon icon={icon} /> {label} ({count})
        </button>
    );
}