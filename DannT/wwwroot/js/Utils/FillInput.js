export function fillInput(inputId,content) {
   let input = document.getElementById(inputId);
    input.textContent = content;
    input.value= content;
}