import { useState, useEffect, useContext } from "react";
import { Context } from "../../App";
import { ReviewsApi } from "../../API/ReviewsApi";

// Custom hook for managing reactions
export const useReaction = (review, user) => {
  const [reaction, setReaction] = useState(null);
  const [likeDislikeLoading, setLikeDislikeLoading] = useState({ like: false, dislike: false });

  // Fetch user's reaction for the review
  const fetchUserReaction = async () => {
    if (user) {
      try {
        const response = await ReviewsApi.getReactionByReviewUser(review.id, user.id);
        if (response.success) {
          setReaction(response.reaction);
        }
      } catch (error) {
        console.error("Error fetching user reaction:", error);
      }
    }
  };

  // Add reaction (like/dislike)
  const addReaction = async (isLike) => {
    setLikeDislikeLoading((prev) => ({ ...prev, [isLike ? "like" : "dislike"]: true }));
    try {
      const response = await ReviewsApi.submitReaction(review.id, isLike);
      if (response.success) {
        setReaction({ reviewId: review.id, userId: user.id, isLike });
      } else {
        alert(response.message);
      }
    } catch (error) {
      alert("Error submitting reaction:", error);
    } finally {
      setLikeDislikeLoading((prev) => ({ ...prev, [isLike ? "like" : "dislike"]: false }));
    }
  };

  // Remove reaction
  const removeReaction = async () => {
    if (!reaction) return;
    setLikeDislikeLoading({ like: true, dislike: true });
    try {
      const response = await ReviewsApi.removeReaction(reaction.id);
      if (response.success) {
        setReaction(null);
      } else {
        alert(response.message);
      }
    } catch (error) {
      alert("Error removing reaction:", error);
    } finally {
      setLikeDislikeLoading({ like: false, dislike: false });
    }
  };

  // Handle add/remove reaction logic
  const handleAddReaction = (isLike) => {
    if (reaction && reaction.isLike === isLike) {
      removeReaction();
    } else {
      addReaction(isLike);
    }
  };

  // Fetch the user's reaction on component mount or when necessary dependencies change
  useEffect(() => {
    fetchUserReaction();
  }, [user, review.id]);

  return { reaction, likeDislikeLoading, handleAddReaction, fetchUserReaction };
};
