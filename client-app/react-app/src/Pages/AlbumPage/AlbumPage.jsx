import { useParams } from "react-router-dom";
import React, { useEffect, useState, useContext } from "react";
import "./AlbumPage.css";
import { AlbumsApi } from "../../API/AlbumsApi";
import ReviewForm from "../../Components/ReviewForm/ReviewForm";
import { Context } from "../../App";
import { ReviewsApi } from "../../API/ReviewsApi";
import Review from "../../Components/Review/Review";

export default function AlbumPage() {
  const { id } = useParams();
  const { isAuthenticated } = useContext(Context);
  const [album, setAlbum] = useState(null);
  const [reviews, setReviews] = useState([]);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [reviewsVisible, setReviewsVisible] = useState(false);

  const fetchAlbum = async (albumId) => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await AlbumsApi.getAlbum(albumId);
      if (!response.success) {
        setError(response.message);
      } else {
        setAlbum(response.album);
      }
    } catch (error) {
      setError("Unexpected error while fetching album details. Try again.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleReviewSubmit = async (review) => {
    try {
      const response = await ReviewsApi.submitReview(id, review);
      if(response.success) {
        fetchReviews();
        fetchAlbum(id);
      } else {
        alert(response.message);
      }
    } catch (error) {
      alert("Unexpected error while trying to submit review. Try again.");
    }
  };

  const handleViewReviews = async () => {
    setReviewsVisible(true);
    fetchReviews();
  };

  const fetchReviews = async () => {
    try {
      const response = await ReviewsApi.getReviewsByAlbum(id);
      if (!response.success) {
        alert(response.message);
      } else {
        setReviews(response.reviews);
      }
    } catch (error) {
      alert("Unexpected error while trying to load reviews. Try again.");
    }
  };

  useEffect(() => {
    if (id) {
      fetchAlbum(id);
    }
  }, [id]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return (
      <div>
        <p>Error: {error}</p>
      </div>
    );
  }

  if (!album) {
    return <p>No album data available</p>;
  }

  return (
    <div className="album-page">
        <div className="album-wrapper">
            <div className="album-container">
                    <h2>Album info</h2>
                    <div className="album-info">
                        <p>Album name: {album.name}</p>
                        {album.artists && <p>By: {album.artists}</p>}
                        {album.genre && <p>Genre: {album.genre}</p>}
                        {album.averageRating !== 0 && <p>Average rating: {album.averageRating}/10</p>}
                        {album.releaseDate && (
                        <p>Released on: {new Date(album.releaseDate).toLocaleDateString()}</p>
                        )}
                    </div>
            </div>
            <div className="reviews-container">
                {isAuthenticated ? (
                    <ReviewForm onSubmit={handleReviewSubmit} />
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
                    reviews.map((review) => (
                        <Review key={review.id} review={review} />
                    ))
                    ) : (
                    <p>No reviews yet.</p>
                    )}
                </div>
                )}
            </div>
        </div>
      </div>
    </div>
  );
}