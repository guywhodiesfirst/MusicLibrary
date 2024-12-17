import { client } from "./client";

export class AlbumsApi {
    static async searchAlbums(query) {
        try {
            const response = await client(`MusicBrainz/albums/search?query=${encodeURIComponent(query)}`);
            if(response.error) {
                return {
                    success: false,
                    message: response.message | "An error occured while fetching the albums"
                }
            } else if(response.length === 0) {
                return {
                    success: true,
                    albums: []
                }
            } else {
                return {
                    success: true,
                    albums: response
                }
            }

        } catch(error) {
            return {
                success: false,
                message: error.message
            }
        }
    }

    static async getAlbum(id) {
        try {
            const response = await client(`albums/${id}/details`);
            if(response.error) {
                return {
                    success: false,
                    message: response.message | "An error occured while fentching the album"
                }
            } else {
                return {
                    success: true,
                    album: response
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