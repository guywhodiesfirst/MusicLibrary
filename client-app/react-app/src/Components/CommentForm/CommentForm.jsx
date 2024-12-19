import React, { useState } from "react";
import "./CommentForm.css";

export default function CommentForm({ onSubmit }) {
  const [comment, setComment] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = () => {
    if (!comment.trim()) {
      setError("Comment cannot be empty");
      return;
    }

    if (comment.length > 300) {
      setError("Comment cannot exceed 300 characters");
      return;
    }

    setError("");
    onSubmit(comment);
    setComment("");
  };

  const handleChange = (e) => {
    const value = e.target.value;
    if (value.length <= 300) {
      setComment(value);
      setError("");
    } else {
      setError("Comment cannot exceed 300 characters");
    }
  };

  return (
    <div>
      <div>
        <textarea
          value={comment}
          onChange={handleChange}
          placeholder="Write your comment here..."
          rows="10"
          cols="30"
        ></textarea>
      </div>
      {error && <p className="error">{error}</p>}
      <p className="char-count">{comment.length} / 300</p>
      <button onClick={handleSubmit}>Submit Comment</button>
    </div>
  );
}