const commentUpvoteBtns = document.querySelectorAll('.comment-upvote-btn');
const commentDownvoteBtns = document.querySelectorAll('.comment-downvote-btn');
const commentRatings = document.querySelectorAll('.comment-rating');

commentUpvoteBtns.forEach(btn => {
    btn.onclick = () => {
        const commentId = btn.dataset.commentId;
        window.fetch(`/api/CommentVote/Upvote/${encodeURIComponent(commentId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        })
        .then(() => window.fetch(`/api/CommentVote/Votes/${encodeURIComponent(commentId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        }))
        .then(res => res.text())
        .then(rating => {
            btn.classList.remove('text-black-50');
            btn.classList.add('text-danger');

            const downvoteBtn = Array.from(commentDownvoteBtns).find(b => b.dataset.commentId === commentId);
            downvoteBtn.classList.remove('text-danger');
            downvoteBtn.classList.add('text-black-50');

            const ratingText = Array.from(commentRatings).find(r => r.dataset.commentId === commentId);
            ratingText.textContent = rating;
        });
    }
});

commentDownvoteBtns.forEach(btn => {
    btn.onclick = () => {
        const commentId = btn.dataset.commentId;
        window.fetch(`/api/CommentVote/Downvote/${encodeURIComponent(commentId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        })
        .then(() => window.fetch(`/api/CommentVote/Votes/${encodeURIComponent(commentId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        }))
        .then(res => res.text())
        .then(rating => {
            btn.classList.remove('text-black-50');
            btn.classList.add('text-danger');

            const upvoteBtn = Array.from(commentUpvoteBtns).find(b => b.dataset.commentId === commentId);
            upvoteBtn.classList.remove('text-danger');
            upvoteBtn.classList.add('text-black-50');

            const ratingText = Array.from(commentRatings).find(r => r.dataset.commentId === commentId);
            ratingText.textContent = rating;
        });
    }
});