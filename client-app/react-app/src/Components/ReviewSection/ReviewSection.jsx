import React, { useEffect, useState, useContext } from "react";
import { Context } from "../../App";
import { ReviewsApi } from "../../API/ReviewsApi";
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
    const { isAuthenticated, user } = useContext(Context);
    const [isLoading, setIsLoading] = useState(false);

    const handleReactionUpdate = () => {
        console.log("УРАААА")
        onReviewUpdates(); // Фетчимо огляди після оновлення реакцій
        console.log(reviews)
    };

    return (
        <div className="reviews-container">
            {isAuthenticated ? (
                <ReviewForm
                    userReview={userReview}
                    onSubmit={handleReviewSubmit}
                    onUpdate={handleReviewUpdate}
                    onDelete={handleReviewDelete}
                />
            ) : (
                <h3>Please authorize to review</h3>
            )}
            <div className="view-reviews-section">
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
