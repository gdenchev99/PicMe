export function showEmojiPicker() {
    let emojiPicker = document.getElementById("emoji-picker");

    document.getElementById("btn-emoji").addEventListener("click" function () {
        emojiPicker.style.display = "hidden";
    })
}