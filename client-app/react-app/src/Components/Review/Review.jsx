import React from "react";
import "./Review.css";

export default function Review({ review }) {
    return (
        <div className="review">
            <div className="review-info">
                <p>
                    <span className="review-info-prop-label">by</span>
                    <span className="review-info-prop">{review.username} •</span>
                    <span className="review-info-prop-label">Rating:</span>
                    <span className="review-info-prop">{review.rating}/10 •</span>
                    {review.content &&
                        <>
                            <span className="review-info-prop-label">Likes:</span>
                            <span className="review-info-prop">{review.likes} •</span>
                            <span className="review-info-prop-label">Dislikes:</span>
                            <span className="review-info-prop">{review.dislikes}</span>
                        </>
                    }
                </p>
                <p>
                    <span className="review-info-prop-label">Created at: </span>
                    <span className="review-info-prop">{new Date(review.createdAt).toLocaleString()} •</span>
                    <span className="review-info-prop-label">Last updated at: </span>
                    <span className="review-info-prop">{new Date(review.lastUpdatedAt).toLocaleString()}</span>
                </p>
            </div>
            {review.content && (
                <div className="review-content">
                    <p>{review.content}</p>
                </div>
            )}
        </div>
    );
}
