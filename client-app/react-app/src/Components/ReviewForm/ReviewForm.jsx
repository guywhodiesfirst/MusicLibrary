import React, { useState } from "react";
import "./ReviewForm.css";

export default function ReviewForm({ onSubmit }) {
  const [content, setContent] = useState("");
  const [rating, setRating] = useState(1);

  const handleSubmit = () => {
    const reviewData = { content, rating };
    onSubmit(reviewData);
    setContent("");
    setRating(1);
  };

  return (
    <div className="review-form">
      <h2>Write a Review</h2>
      <div className="form-data">
        <textarea
            value={content}
            onChange={(e) => setContent(e.target.value)}
            placeholder="Write your review here (optional)..."
            rows="15"
            cols="50"
        ></textarea>
        <br />
        <label className="rating-label">
            Rating: 
        </label>
        <select value={rating} onChange={(e) => setRating(Number(e.target.value))}>
            {Array.from({ length: 10 }, (_, i) => i + 1).map((value) => (
                <option key={value} value={value}>
                {value}
                </option>
            ))}
        </select>
      </div>
      <br />
      <button onClick={handleSubmit}>Submit Review</button>
    </div>
  );
}
