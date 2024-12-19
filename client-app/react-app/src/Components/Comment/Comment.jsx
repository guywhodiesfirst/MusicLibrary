import './Comment.css'

export default function Comment({ comment }) {
    return(
        <div className='comment'>
            <p>
                <span className='comment-info-prop-label'>by</span> 
                <span className='comment-info-prop'>{comment.username} •</span>
                <span className='comment-info-prop-label'>Created at</span>
                <span className='comment-info-prop'>{new Date(comment.createdAt).toLocaleString()}</span>
            </p>
            <p>{comment.content}</p>
        </div>
    )
}