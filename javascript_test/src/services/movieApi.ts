import axios from 'axios';
import { Movie } from '../types/Movie';

const API_KEY = import.meta.env.VITE_OMDB_API_KEY;
const BASE_URL = import.meta.env.VITE_OMDB_BASE_URL;

if (!API_KEY) {
    throw new Error('OMDB API key is not defined in environment variables');
}

if (!BASE_URL) {
    throw new Error('OMDB base URL is not defined in environment variables');
}

export const searchMovies = async (query: string): Promise<Movie[]> => {
    try {
        const response = await axios.get(`${BASE_URL}?apikey=${API_KEY}&s=${query}`);
        if (response.data.Search) {
            return response.data.Search.slice(0, 3);
        }
        return [];
    } catch (error) {
        console.error('Error searching movies:', error);
        return [];
    }
}; 