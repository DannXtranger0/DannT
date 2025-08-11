import { form,Search,SetTasks,selectTagId} from "../Features/Feed.js"
import { LoadTags } from "../Utils/GetTags.js"

document.addEventListener("DOMContentLoaded", async () => {
    let data = await Search(form);
    await SetTasks(data);

    selectTagId.innerHTML += await LoadTags();


})
form.addEventListener("change", async (e) => {
    e.preventDefault();
    let data = await Search(form);
    await SetTasks(data);

})