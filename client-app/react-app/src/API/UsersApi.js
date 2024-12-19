// static async update(userData) {
//     try {
//         const response = await client('profile', {
//             method: 'PUT',
//             body: JSON.stringify(userData),
//         });

//         if (response.error) {
//             return { success: false, message: response.message };
//         }

//         return { success: true, message: response.message };
//     } catch (error) {
//         return { success: false, message: 'Error occurred during update. ' + error.message };
//     }
// }

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
}