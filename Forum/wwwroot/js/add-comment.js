const form = document.querySelector('#add-comment');
const comment = document.querySelector('#add-comment textarea');


form.onsubmit = function (e) {
    e.preventDefault();
    if (!comment.value) {
        return;
    }
    form.submit();
}
