import { client } from "./client";

export class PlaylistApi {
    static async createPlaylist(userId, playlist) {
        try {
            const response = await client(`users/${userId}/playlists`, {
                method: 'POST',
                body: JSON.stringify(playlist),
            });
            return {
                success: !response.error,
                message: response.error ? response.message : "Playlist created successfully"
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async getPlaylistsByUser(userId) {
        try {
            const response = await client(`users/${userId}/playlists`, {
                method: 'GET',
            });
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                playlists: response.error ? undefined : response
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async getById(playlistId) {
        try {
            const response = await client(`playlists/${playlistId}`, {
                method: 'GET',
            });
            return {
                success: !response.error,
                message: response.error ? response.message : undefined,
                playlist: response.error ? undefined : response
            }
        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async updatePlaylist(playlistId, updatedPlaylist) {
        try {
            const response = await client(`playlists/${playlistId}`, {
                method: 'PUT',
                body: JSON.stringify(updatedPlaylist),
            });
            return {
                success: !response.error,
                message: response.error ? response.message : "Playlist updated successfully",
            };
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }    

    static async deletePlaylist(playlistId) {
        try {
            const response = await client(`playlists/${playlistId}`, {
                method: 'DELETE'
            });
            return {
                success: !response.error,
                message: response.error ? response.message : "Playlist deleted successfully",
            };
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

    static async addAlbumToPlaylist(albumId, playlistId) {
        try {
            const response = await client(`playlists/${playlistId}/albums/${albumId}`, {
                method: 'POST'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : "Album added successfully"
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }

    static async removeAlbumFromPlaylist(albumId, playlistId) {
        try {
            const response = await client(`playlists/${playlistId}/albums/${albumId}`, {
                method: 'DELETE'
            })
            return {
                success: !response.error,
                message: response.error ? response.message : "Album removed successfully"
            }
        } catch (error) {
            return {
                success: false,
                message: error.message,
            };
        }
    }
}