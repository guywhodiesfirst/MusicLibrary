import './Comment.css'
import { useNavigate } from 'react-router-dom'

export default function Comment({ comment }) {
    const navigate = useNavigate()
    const handleGoToProfile = () => {
        navigate(`/users/${comment.userId}`);
    }
    return(
        <div className='comment'>
            <p>
                <span className='comment-info-prop-label'>by</span> 
                <span 
                    className='comment-info-prop username'
                    onClick={handleGoToProfile}>
                    {comment.username}</span>
                <span className='comment-info-prop-label'>• Created at</span>
                <span className='comment-info-prop'>{new Date(comment.createdAt).toLocaleString()}</span>
            </p>
            {comment.isDeleted ? (
                <p className='content-blocked'>Comment was blocked from viewing by the administrator.</p>
            ) : (
                <p>{comment.content}</p>
            )
            }
        </div>
    )
}