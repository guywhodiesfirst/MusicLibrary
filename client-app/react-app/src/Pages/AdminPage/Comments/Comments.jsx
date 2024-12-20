import { useEffect, useState } from 'react';
import CommentsTable from './CommentsTable.jsx';
import { AdminApi } from '../../../API/AdminApi.js';
import { CommentsApi } from '../../../API/CommentsApi.js';

export default function Comments() {
    const [comments, setComments] = useState([]);
  
    const fetchComments = async () => {
        try {
            const response = await CommentsApi.getAll();
            if(!response.success) {
                alert(response.message)
            } else {
                setComments(response.comments)
            }
        } catch (error) {
            alert(error)
        }
    }

  useEffect(() => {
    fetchComments();
  }, []);

  const handleBlock = async (comment) => {
    try {
        const response = await AdminApi.blockComment(comment.id);
        if(!response.success) {
            alert(response.message)
        } else {
            await fetchComments()
        }
    } catch (error) {
        alert(error)
    }
  }

  const handleUnblock = async (comment) => {
    try {
        const response = await AdminApi.unblockComment(comment.id);
        if(!response.success) {
            alert(response.message)
        } else {
            await fetchComments()
        }
    } catch (error) {
        alert(error)
    }
  }

  const handleBlockButtonClick = async (comment) => {
    if(comment.isDeleted) {
        await handleUnblock(comment)
    } else {
        await handleBlock(comment)
    }
  }

  return (
    <>
      <CommentsTable comments={comments} onBlock={handleBlockButtonClick} />
    </>
  );
}