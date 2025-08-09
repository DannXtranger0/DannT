import { form,Create,LoadTags} from "../Features/CreateTask.js"

document.addEventListener("DOMContentLoaded", async () => {
   await LoadTags();
})
form.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await Create(form)
    if (res)
        window.location.href = "/Feed";
})