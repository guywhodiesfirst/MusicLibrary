import './PlaylistInfo.css'

export default function PlaylistInfo({ playlist }) {
  return (
    <div>
      <h2>Playlist info</h2>
      <div className="playlist-info-block">
        <p>Name: {playlist.name}</p>
        {playlist.description && <p>Description: {playlist.description}</p>}
        <p>Albums: {playlist.albumCount}</p>
        <p>By: {playlist.username}</p>
        <p>Created at: {new Date(playlist.createdAt).toLocaleDateString()}</p>
      </div>
    </div>
  );
}