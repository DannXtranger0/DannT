import { form, register } from "../Features/Register.js"

form.addEventListener("submit", async (e) => {
    e.preventDefault();
    await register(form);
})