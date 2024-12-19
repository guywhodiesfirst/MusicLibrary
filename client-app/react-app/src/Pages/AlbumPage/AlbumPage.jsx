import { useParams } from "react-router-dom";
import React, { useEffect, useState, useContext } from "react";
import { Context } from "../../App";
import "./AlbumPage.css";
import { AlbumsApi } from "../../API/AlbumsApi";
import { ReviewsApi } from "../../API/ReviewsApi";
import AlbumInfo from "../../Components/AlbumInfo/AlbumInfo";
import ReviewSection from "../../Components/ReviewSection/ReviewSection";
import Loader from "../../Components/UI/Loader/Loader";
import ErrorMessage from "../../Components/UI/ErrorMessage/ErrorMessage";

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
    } catch {
      setError("Unexpected error while fetching album details. Try again.");
    }
  };

  const fetchUserReview = async () => {
    if (!user) return;
    try {
      const response = await ReviewsApi.getByAlbumUser(id, user.id);
      if (response.success) {
        setUserReview(response.review);
      }
    } catch {
      console.error("Failed to fetch user review.");
    }
  };

  const fetchReviews = async () => {
    try {
      setIsLoading(true)
      const response = await ReviewsApi.getReviewsByAlbum(id);
      if (response.success) {
        setReviews(response.reviews);
      } else {
        alert(response.message);
      }
    } catch(error) {
      alert(error);
    } finally {
      setIsLoading(false)
    }
  };

  const handleReviewSubmit = async (review) => {
    try {
      const response = await ReviewsApi.submitReview(id, review);
      if (response.success) {
        await fetchReviews();
        await fetchAlbum(id);
        await fetchUserReview();
      } else {
        alert(response.message);
      }
    } catch {
      alert("Unexpected error while trying to submit review. Try again.");
    }
  };

  const handleReviewUpdate = async (review) => {
    if (!userReview) return;
    try {
      setIsLoading(true)
      const response = await ReviewsApi.updateReview(userReview.id, review);
      if (response.success) {
        await fetchReviews();
        await fetchAlbum(id);
        await fetchUserReview();
      } else {
        alert(response.message);
      }
    } catch {
      alert("Unexpected error while trying to update review. Try again.");
    } finally {
      setIsLoading(false)
    }
  };

  const handleReviewDelete = async () => {
    if (!userReview) return;
    try {
      setIsLoading(true)
      const response = await ReviewsApi.deleteReview(userReview.id);
      if (response.success) {
        setUserReview(null);
        await fetchReviews();
        await fetchAlbum(id);
      } else {
        alert(response.message);
      }
    } catch {
      alert("Unexpected error while trying to delete review. Try again.");
    } finally {
      setIsLoading(false)
    }
  };

  const handleViewReviews = async () => {
    setReviewsVisible(true);
    fetchReviews();
  };

  const fetchData = async () => {
    setIsLoading(true);
    try {
      await fetchAlbum(id);
      if (isAuthenticated && user) {
        await fetchUserReview();
      }
      await fetchReviews();
    } catch {
      setError("Unexpected error while fetching data. Try again.");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (id) fetchData();
  }, [id, isAuthenticated, user]);

  if (isLoading) return <Loader />;
  if (error) return <ErrorMessage message={error} />;
  if (!album) return <p>No album data available</p>;

  return (
    <div className="album-page">
      <div className="album-wrapper">
        <AlbumInfo album={album} />
        <ReviewSection
          isAuthenticated={isAuthenticated}
          userReview={userReview}
          handleReviewSubmit={handleReviewSubmit}
          handleReviewUpdate={handleReviewUpdate}
          handleReviewDelete={handleReviewDelete}
          reviewsVisible={reviewsVisible}
          reviews={reviews}
          album={album}
          handleViewReviews={handleViewReviews}
          onReviewUpdates={fetchReviews}
        />
      </div>
    </div>
  );
}