import styled from '@emotion/styled';
import { Movie } from '../types/Movie';
import React from 'react';

const Card = styled.div`
    border-radius: 8px;
    overflow: hidden;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s;
    background: white;
    
    &:hover {
        transform: translateY(-5px);
    }
`;

const PosterContainer = styled.a`
    display: block;
    position: relative;
    padding-top: 150%;
    overflow: hidden;
    cursor: pointer;
`;

const Poster = styled.img`
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
`;

const Info = styled.div`
    padding: 1rem;
    text-align: center;
`;

const Title = styled.h3`
    margin: 0;
    font-size: 1.1rem;
    color: #2c3e50;
`;

const Year = styled.p`
    margin: 0.5rem 0 0;
    color: #7f8c8d;
`;

interface MovieCardProps {
    movie: Movie;
}

export const MovieCard = ({ movie }: MovieCardProps) => {
    const imdbUrl = `https://www.imdb.com/title/${movie.imdbID}`;
    
    return (
        <Card>
            <PosterContainer href={imdbUrl} target="_blank" rel="noopener noreferrer">
                <Poster 
                    src={movie.Poster !== 'N/A' ? movie.Poster : 'https://via.placeholder.com/300x450?text=No+Poster'} 
                    alt={`${movie.Title} poster`}
                />
            </PosterContainer>
            <Info>
                <Title>{movie.Title}</Title>
                <Year>{movie.Year}</Year>
            </Info>
        </Card>
    );
}; 