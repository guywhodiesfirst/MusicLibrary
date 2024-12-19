import { client } from "./client";

export class CommentsApi {
    static async submitComment(reviewId, comment) {
        try {
            const response = await client(`reviews/${reviewId}/comments`, {
                method: 'POST',
                body: JSON.stringify({
                    content: comment
                })
            });
            return {
                success: !response.error,
                message: response.error ? response.message : "Comment submitted!"
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async getCommentsByReview(reviewId) {
        try {
            const response = await client(`reviews/${reviewId}/comments`, {
                method: 'GET',
            });
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                comments: response.error ? undefined : response
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }
}