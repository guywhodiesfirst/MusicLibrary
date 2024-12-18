import { client } from "./client.js";

export class AuthApi {
    static async register(formData) {
        if (formData.password !== formData.passwordConfirm) {
            return {
                success: false,
                message: "Password fields don't match!",
            };
        }

        try {
            const response = await client('auth/register', {
                method: 'POST',
                body: JSON.stringify(formData),
            });

            if (response.error) {
                return {
                    success: false,
                    message: response.message,
                };
            }

            return {
                success: true,
                message: response.message,
            };
        } catch (error) {
            return {
                success: false,
                message: 'Error occurred during registration. ' + error.message,
            };
        }
    }

    static async login(formData) {
        console.log(JSON.stringify(formData))
        try {
            const response = await client('auth/login', {
                method: 'POST',
                body: JSON.stringify(formData),
            });
            
            if (response.error) {
                return {
                    success: false,
                    message: "Authorization error"
                }
            } 
            
            return {
                success: true,
                access_token: response.access_token
            }
        } catch (error) {
            return {
                success: false,
                message: error.message
            }
        }
    }
}