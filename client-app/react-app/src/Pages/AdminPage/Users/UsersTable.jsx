import '../AdminPage.css'
const UsersTable = ({ users, onBlock }) => {
    return (
      <table className="table">
        <thead>
        <tr>
          <th>ID</th>
          <th>Username</th>
          <th>Email</th>
          <th>Is blocked</th>
          <th>Is admin</th>
          <th>Block</th>
        </tr>
        </thead>
        <tbody>
        {users.map((user) => (
          <tr key={user.id}>
            <td>{user.id}</td>
            <td>{user.username}</td>
            <td>{user.email}</td>
            <td>{user.isBlocked ? <>True</> : <>False</>}</td>
            <td>{user.isAdmin ? <>True</> : <>False</>}</td>
            <td>
              <button className="edit-button" onClick={() => onBlock(user)}>{user.isBlocked ? <>Unblock</> : <>Block</>}</button>
            </td>
          </tr>
        ))}
        </tbody>
      </table>
    );
  };
  
  export default UsersTable;