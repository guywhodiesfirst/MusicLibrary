import React, { useState } from "react";
import "./UserAboutForm.css";

export default function UserAboutForm({ onSubmit }) {
  const [about, setAbout] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = () => {
    if (about.length > 500) {
      setError("User description cannot exceed 500 characters");
      return;
    }

    setError("");
    onSubmit(about);
    setAbout("");
  };

  const handleChange = (e) => {
    const value = e.target.value;
    if (value.length <= 500) {
      setAbout(value);
      setError("");
    } else {
      setError("User description cannot exceed 500 characters");
    }
  };

  return (
    <div>
      <div>
        <textarea
          value={about}
          onChange={handleChange}
          placeholder="Write your description here..."
          rows="10"
          cols="30"
        ></textarea>
      </div>
      {error && <p className="error">{error}</p>}
      <p>{about.length} / 500</p>
      <button onClick={handleSubmit}>Update description</button>
    </div>
  );
}