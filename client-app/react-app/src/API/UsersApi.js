import { client } from "./client.js";

export class UsersApi {
    static async getCurrentUser() {
        try {
            const response = await client('users/me', {
                method: 'GET',
            });
    
            if (!response || response.error) {
                return {
                    success: false,
                    message: response.message || 'Failed to fetch user data.',
                };
            }
    
            return {
                success: true,
                data: response,
            };
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while fetching user data. ' + error.message,
            };
        }
    }

    static async getUserDetails(userId) {
        try {
            const response = await client(`users/${userId}/details`, {
                method: 'GET'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                user: response.error ? undefined : response
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while fetching user data. ' + error.message,
            };
        }
    }

    static async getAll() {
        try {
            const response = await client(`users`, {
                method: 'GET'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                users: response.error ? undefined : response
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while fetching users.'
            };
        }
    }

    static async updateUserAboutSection(userId, about) {
        try {
            const response = await client(`users/${userId}`, {
                method: 'PUT',
                body: JSON.stringify({
                    about: about
                })
            })
            return {
                success: !response.error,
                message: response.error ? response.message : "User info updated successfully"
            }
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred while updating user data. ' + error.message,
            };
        }
    }
}