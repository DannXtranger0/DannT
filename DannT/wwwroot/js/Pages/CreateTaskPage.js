import { form,selectTagId,Create} from "../Features/CreateTask.js"
import { LoadTags } from "../Utils/GetTags.js"

document.addEventListener("DOMContentLoaded", async () => {
    selectTagId.innerHTML += await LoadTags();
})
form.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await Create(form)
    if (res)
        window.location.href = "/Feed";
})