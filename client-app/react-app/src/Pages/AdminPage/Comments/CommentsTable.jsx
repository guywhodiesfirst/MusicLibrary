import '../AdminPage.css';

const CommentsTable = ({ comments, onBlock }) => {
  // Функція для обрізання тексту
  const truncateText = (text, maxLength) => {
    if (text.length > maxLength) {
      return text.slice(0, maxLength) + '...';
    }
    return text;
  };

  return (
    <table className='table'>
      <thead>
        <tr>
          <th>ID</th>
          <th>Review ID</th>
          <th>Username</th>
          <th>Content</th>
          <th>Created at</th>
          <th>Is blocked</th>
          <th>Block</th>
        </tr>
      </thead>
      <tbody>
        {comments.map((comment) => (
          <tr key={comment.id}>
            <td>{comment.id}</td>
            <td>{comment.reviewId}</td>
            <td>{comment.username}</td>
            <td>{truncateText(comment.content, 50)}</td> {/* Обмеження до 50 символів */}
            <td>{new Date(comment.createdAt).toLocaleString()}</td>
            <td>{comment.isDeleted ? <>True</> : <>False</>}</td>
            <td>
              <button onClick={() => onBlock(comment)}>
                {comment.isDeleted ? <>Unblock</> : <>Block</>}
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default CommentsTable;
