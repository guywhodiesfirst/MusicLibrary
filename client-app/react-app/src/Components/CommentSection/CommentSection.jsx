import React, { useState, useContext } from "react";
import { Context } from "../../App";
import './CommentSection.css'
import CommentForm from "../CommentForm/CommentForm";
import Comment from "../Comment/Comment";

export default function CommentSection({
  handleCommentSubmit,
  commentsVisible,
  comments
}) {
    const { isAuthenticated } = useContext(Context);

    return (
        <div>
            <div>
                {commentsVisible && (
                    <>
                        <h3>Comments</h3>
                        {isAuthenticated ? (
                            <CommentForm
                                    onSubmit={handleCommentSubmit}
                                />
                            ) : (
                                <h3>Please authorize to comment</h3>
                        )}
                        <div className="comments-list">
                            {comments.length > 0 ? (
                                comments.map((comment) => <Comment key={comment.id} comment={comment}/>)
                            ) : (
                                <p>No comments yet</p>
                            )    
                            }
                        </div>
                    </>
                )}
            </div>
        </div>
    );
}