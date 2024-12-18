import { client } from "./client";

export class ReviewsApi {
    static async submitReview(albumId, review) {
        console.log(albumId, review)
        try {
            const response = await client(`albums/${albumId}/reviews`, {
                method: 'POST',
                body: JSON.stringify(review),
            });
            if(response.error) {
                return {
                    success: false,
                    message: response.message
                }
            } else {
                return {
                    success: true,
                    message: "Review submitted!"
                }
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async getReviewsByAlbum(albumId) {
        try {
            const response = await client(`albums/${albumId}/reviews`, {
                method: 'GET',
            });
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                reviews: response.error ? undefined : response.reviews
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async getById(reviewId) {
        try {
            const response = await client(`reviews/${reviewId}`, {
                method: 'GET',
            });
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                review: response.error ? undefined : response.review
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async updateReview(reviewId, updatedReview) {
        try {
            const response = await client(`reviews/${reviewId}`, {
                method: 'PUT',
                body: JSON.stringify(updatedReview),
            });
            return {
                success: !response.error,
                message: response.error ? response.message : "Review updated successfully",
            };
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }    

    static async deleteReview(reviewId) {
        try {
            const response = await client(`reviews/${reviewId}`, {
                method: 'DELETE'
            });
            return {
                success: !response.error,
                message: response.error ? response.message : "Review updated successfully",
            };
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }

    static async getByAlbumUser(albumId, userId) {
        try {
            const response = await client(`reviews/by-album-user?albumId=${albumId}&userId=${userId}`, {
                method: 'GET'
            });

            return {
                success: !response.error,
                review: response.error ? undefined : response
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }

    static async getReactionByReviewUser(reviewId, userId) {
        try {
            const response = await client(`reviews/reactions/by-review-user?reviewId=${reviewId}&userId=${userId}`, {
                method: 'GET'
            })
            return {
                success: !response.error,
                reaction: response.error ? null : response
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }

    static async submitReaction(reviewId, isLike) {
        try {
            const response = await client(`reviews/${reviewId}/reactions`, {
                method: 'POST',
                body: JSON.stringify({
                    isLike: isLike,
                })
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }

    static async removeReaction(reactionId) {
        try {
            const response = await client(`reviews/reactions/${reactionId}`, {
                method: 'DELETE'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }

    static async updateReaction(reactionId) {
        try {
            const response = await client(`reviews/reactions/${reactionId}`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }
}