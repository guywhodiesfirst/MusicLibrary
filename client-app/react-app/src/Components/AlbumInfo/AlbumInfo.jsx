import './AlbumInfo.css'

export default function AlbumInfo({ album }) {
  return (
    <div className="album-container">
      <h2>Album info</h2>
      <div className="album-info">
        <p>Album name: {album.name}</p>
        {album.artists && <p>By: {album.artists}</p>}
        {album.genre && <p>Genre: {album.genre}</p>}
        {album.averageRating !== 0 && <p>Average rating: {album.averageRating}/10</p>}
        {album.releaseDate && (
          <p>Released on: {new Date(album.releaseDate).toLocaleDateString()}</p>
        )}
      </div>
    </div>
  );
}