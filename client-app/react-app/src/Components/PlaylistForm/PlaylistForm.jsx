import React, { useState } from "react";
import "./PlaylistForm.css";

export default function PlaylistForm({ onSubmit, infoText}) {
  const [description, setDescription] = useState("");
  const [name, setName] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = () => {
    if (description.length > 400) {
      setError("Description cannot exceed 400 characters.");
      return;
    }
    if(name === "") {
        setError("Playlist name should be filled")
        return;
    }

    if (name.length > 80) {
        setError("Playlist name cannot exceed 80 characters.");
        return;
    }
    const playlistData = { name, description };
    onSubmit(playlistData);
    setError("");
    setDescription("");
    setName("");
  };

  const handleDescriptionChange = (e) => {
    const value = e.target.value;
    if (value.length <= 400) {
      setDescription(value);
      setError("");
    } else {
      setError("Description cannot exceed 400 characters.");
    }
  };

  const handleNameChange = (e) => {
    const value = e.target.value;
    if (value.length <= 80) {
      setName(value);
      setError("");
    } else {
      setError("Playlist name cannot exceed 80 characters.");
    }
  }

  return (
    <div className="playlist-form">
      <h2>{infoText} Playlist</h2>
      <div className="playlist-form-data">
        <input
            type="text"
            value={name}
            onChange={handleNameChange}
            placeholder="Enter playlist name"
            className="playlist-name-input"
        />
        <p>{name.length} / 80</p>
        <textarea
            value={description}
            onChange={handleDescriptionChange}
            placeholder="Write description here (optional)..."
            className="playlist-description-input"
            rows="10"
        ></textarea>
        <p>{description.length} / 400</p>
        {error && <p className="error">{error}</p>}
      </div>
      <br />
      <button onClick={handleSubmit}>{infoText}</button>
    </div>
  );
}
