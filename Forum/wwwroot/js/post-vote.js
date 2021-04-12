const postUpvoteBtns = document.querySelectorAll('.post-upvote-btn');
const postDownvoteBtns = document.querySelectorAll('.post-downvote-btn');
const postRatings = document.querySelectorAll('.post-rating');


postUpvoteBtns.forEach(btn => {
    btn.onclick = () => {
        const postId = btn.dataset.postId;
        window.fetch(`/api/PostVote/Upvote/${encodeURIComponent(postId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        })
        .then(() => window.fetch(`/api/PostVote/Votes/${encodeURIComponent(postId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        }))
        .then(res => res.text())
        .then(rating => {
            btn.classList.remove('text-white');
            btn.classList.add('text-danger');

            const downvoteBtn = Array.from(postDownvoteBtns).find(b => b.dataset.postId === postId);
            downvoteBtn.classList.remove('text-danger');
            downvoteBtn.classList.add('text-white');

            const ratingText = Array.from(postRatings).find(r => r.dataset.postId === postId);
            ratingText.textContent = rating;
        });
    }
});

postDownvoteBtns.forEach(btn => {
    btn.onclick = () => {
        const postId = btn.dataset.postId;
        window.fetch(`/api/PostVote/Downvote/${encodeURIComponent(postId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        })
        .then(() => window.fetch(`/api/PostVote/Votes/${encodeURIComponent(postId)}`, {
            method: 'GET',
            credentials: 'same-origin',
        }))
        .then(res => res.text())
        .then(rating => {
            btn.classList.remove('text-white');
            btn.classList.add('text-danger');

            const upvoteBtn = Array.from(postUpvoteBtns).find(b => b.dataset.postId === postId);
            upvoteBtn.classList.remove('text-danger');
            upvoteBtn.classList.add('text-white');

            const ratingText = Array.from(postRatings).find(r => r.dataset.postId === postId);
            ratingText.textContent = rating;
        });
    }
});