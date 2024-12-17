import { client } from "./client";

export class ReviewsApi {
    static async submitReview(albumId, review) {
        console.log(albumId, review)
        try {
            const response = await client(`albums/${albumId}/reviews`, {
                method: 'POST',
                body: JSON.stringify(review),
            });
            if(!response.success) {
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
            if(!response.success) {
                return {
                    success: false,
                    message: response.message
                }
            } else {
                return {
                    success: true,
                    reviews: response.reviews
                }
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }
}