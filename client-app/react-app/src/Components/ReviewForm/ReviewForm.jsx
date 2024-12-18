import React, { useState, useEffect } from "react";
import "./ReviewForm.css";

export default function ReviewForm({ userReview, onSubmit, onUpdate, onDelete }) {
  const [content, setContent] = useState(userReview?.content || "");
  const [rating, setRating] = useState(userReview?.rating || 1);

  useEffect(() => {
    if (userReview) {
      setContent(userReview.content);
      setRating(userReview.rating);
    }
  }, [userReview]);

  const handleSubmit = () => {
    const reviewData = { content, rating };
    userReview ? onUpdate(reviewData) : onSubmit(reviewData);
    if (!userReview) {
      setContent("");
      setRating(1);
    }
  };

  const handleDelete = () => {
    onDelete()
    setContent('')
  }

  return (
    <div className="review-form">
      <h2>{userReview ? "Edit your Review" : "Write a Review"}</h2>
      <div className="form-data">
        <textarea
          value={content}
          onChange={(e) => setContent(e.target.value)}
          placeholder="Write your review here (optional)..."
          rows="15"
          cols="50"
        ></textarea>
        <br />
        <label className="rating-label">Rating:</label>
        <select value={rating} onChange={(e) => setRating(Number(e.target.value))}>
          {Array.from({ length: 10 }, (_, i) => i + 1).map((value) => (
            <option key={value} value={value}>
              {value}
            </option>
          ))}
        </select>
      </div>
      <br />
      <button onClick={handleSubmit}>{userReview ? "Update Review" : "Submit Review"}</button>
      {userReview && <button onClick={handleDelete} style={{marginLeft: "10px"}}>Delete Review</button>}
    </div>
  );
}
