import '../AdminPage.css';

const ReviewsTable = ({ reviews, onBlock }) => {
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
          <th>Username</th>
          <th>Album name</th>
          <th>Content</th>
          <th>Rating</th>
          <th>Created at</th>
          <th>Is blocked</th>
          <th>Block</th>
        </tr>
      </thead>
      <tbody>
        {reviews.map((review) => (
          <tr key={review.id}>
            <td>{review.id}</td>
            <td>{review.username}</td>
            <td>{truncateText(review.albumName, 50)}</td> {/* Обмеження до 50 символів */}
            <td>{review.content ? truncateText(review.content, 100) : ''}</td>
            <td>{review.rating}</td>
            <td>{new Date(review.createdAt).toLocaleString()}</td>
            <td>{review.isDeleted ? <>True</> : <>False</>}</td>
            <td>
              <button onClick={() => onBlock(review)}>
                {review.isDeleted ? <>Unblock</> : <>Block</>}
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default ReviewsTable;
