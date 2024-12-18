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
  const { isAuthenticated, user } = useContext(Context);
  const [album, setAlbum] = useState(null);
  const [userReview, setUserReview] = useState(null);
  const [reviews, setReviews] = useState([]);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [reviewsVisible, setReviewsVisible] = useState(false);

  const fetchAlbum = async (albumId) => {
    setError(null);

    try {
      const response = await AlbumsApi.getAlbum(albumId);
      if (!response.success) {
        setError(response.message);
      } else {
        setAlbum(response.album);
      }
    } catch (error) {
      setError("Unexpected error while fetching album details. Try again.");
    }
  };

  const fetchUserReview = async () => {
    try {
      const response = await ReviewsApi.getByAlbumUser(id, user.id);
      console.log(response)
      if (response.success) {
        setUserReview(response.review);
      }
    } catch (error) {
      console.error("Failed to fetch user review:", error.message);
    }
  };

  const handleReviewSubmit = async (review) => {
    try {
      const response = await ReviewsApi.submitReview(id, review);
      if (response.success) {
        fetchReviews();
        fetchAlbum(id);
        fetchUserReview();
      } else {
        alert(response.message);
      }
    } catch (error) {
      alert("Unexpected error while trying to submit review. Try again.");
    }
  };

  const handleReviewUpdate = async (review) => {
    try {
      const response = await ReviewsApi.updateReview(userReview.id, review);
      if (response.success) {
        fetchReviews();
        fetchAlbum(id);
        fetchUserReview();
      } else {
        alert(response.message);
      }
    } catch (error) {
      alert("Unexpected error while trying to update review. Try again.");
    }
  };

  const handleReviewDelete = async () => {
    setIsLoading(true)
    try {
      const response = await ReviewsApi.deleteReview(userReview.id);
      if (response.success) {
        fetchReviews();
        fetchAlbum(id);
        setUserReview(null);
      } else {
        alert(response.message);
      }
    } catch (error) {
      alert("Unexpected error while trying to delete review. Try again.");
    }
    setIsLoading(false)
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
    setIsLoading(true)
    if (id) {
      fetchAlbum(id);
      if (isAuthenticated && user) {
        fetchUserReview();
      }
    }
    setIsLoading(false)
  }, [id, isAuthenticated, user]);

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
                  reviews.map((review) => <Review key={review.id} review={review} />)
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