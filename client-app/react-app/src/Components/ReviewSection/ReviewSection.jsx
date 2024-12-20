import React, { useState, useContext } from "react";
import { Context } from "../../App";
import Review from "../Review/Review";
import ReviewForm from "../ReviewForm/ReviewForm";
import './ReviewSection.css'

export default function ReviewSection({
  userReview,
  handleReviewSubmit,
  handleReviewUpdate,
  handleReviewDelete,
  reviewsVisible,
  reviews,
  album,
  handleViewReviews,
  onReviewUpdates
}) {
    const { isAuthenticated, isBlocked } = useContext(Context);

    const handleReactionUpdate = () => {
        onReviewUpdates();
    };

    return (
        <div>
            {isAuthenticated ? (
                isBlocked ? (
                    <div className="content-blocked">You were blocked from posting by the administrator.</div>
                ) : (
                <ReviewForm
                    userReview={userReview}
                    onSubmit={handleReviewSubmit}
                    onUpdate={handleReviewUpdate}
                    onDelete={handleReviewDelete}
                />)
            ) : (
                <h3>Please authorize to review</h3>
            )}
            <div>
                <h2>Reviews</h2>
                <button
                    onClick={handleViewReviews}
                    disabled={album.reviewCount === 0 || reviewsVisible}
                    className="view-reviews-btn"
                >
                    View Reviews({album.reviewCount})
                </button>
                {reviewsVisible && (
                    <div className="reviews-list">
                        {reviews.length > 0 ? (
                            reviews.map((review) => <Review key={review.id} review={review} onReviewChange={handleReactionUpdate}/>)
                        ) : (
                            <p>No reviews yet.</p>
                        )}
                    </div>
                )}
            </div>
        </div>
    );
}