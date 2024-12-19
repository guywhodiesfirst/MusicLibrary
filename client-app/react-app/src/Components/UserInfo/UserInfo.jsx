import './UserInfo.css'

export default function UserInfo({ user }) {
    console.log(user)
  return (
    <div>
      <h2>User info</h2>
      <div className="user-info">
        <p>Username: {user.username}</p>
        <p>Email: {user.email}</p>
        <p>Reviews: {user.reviewCount}</p>
        <p>Playlists: {user.playlistCount}</p>
        {user.about && <p>About: {user.about}</p>}
      </div>
    </div>
  );
}