import { client } from "./client.js";

export class AdminApi {
    static async blockUser(userId) {
        try {
            const response = await client(`admin/users/${userId}/block`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while trying to block user.'
            };
        }
    }
    static async blockReview(reviewId) {
        try {
            const response = await client(`admin/reviews/${reviewId}/block`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while trying to block review.'
            };
        }
    }
    static async blockComment(commentId) {
        try {
            const response = await client(`admin/comments/${commentId}/block`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while trying to block comment.'
            };
        }
    }
    static async unblockUser(userId) {
        try {
            const response = await client(`admin/users/${userId}/unblock`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while trying to unblock user.'
            };
        }
    }
    static async unblockReview(reviewId) {
        try {
            const response = await client(`admin/reviews/${reviewId}/unblock`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while trying to unblock review.'
            };
        }
    }
    static async unblockComment(commentId) {
        try {
            const response = await client(`admin/comments/${commentId}/unblock`, {
                method: 'PUT'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while trying to unblock comment.'
            };
        }
    }
}