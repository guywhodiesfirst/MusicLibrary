import { useCallback, useEffect, useState } from 'react';
import ReviewsTable from './ReviewsTable.jsx';
import { ReviewsApi } from '../../../API/ReviewsApi.js';
import { AdminApi } from '../../../API/AdminApi.js';

export default function Reviews() {
    const [reviews, setReviews] = useState([]);
  
    const fetchReviews = async () => {
        try {
            const response = await ReviewsApi.getAll();
            if(!response.success) {
                alert(response.message)
            } else {
                setReviews(response.reviews)
            }
        } catch (error) {
            alert(error)
        }
    }

  useEffect(() => {
    fetchReviews();
  }, []);

  const handleBlock = async (review) => {
    try {
        const response = await AdminApi.blockReview(review.id);
        if(!response.success) {
            alert(response.message)
        } else {
            await fetchReviews()
        }
    } catch (error) {
        alert(error)
    }
  }

  const handleUnblock = async (review) => {
    try {
        const response = await AdminApi.unblockReview(review.id);
        if(!response.success) {
            alert(response.message)
        } else {
            await fetchReviews()
        }
    } catch (error) {
        alert(error)
    }
  }

  const handleBlockButtonClick = async (review) => {
    if(review.isDeleted) {
        await handleUnblock(review)
    } else {
        await handleBlock(review)
    }
  }

  return (
    <>
      <ReviewsTable reviews={reviews} onBlock={handleBlockButtonClick} />
    </>
  );
}