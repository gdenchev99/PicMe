import axios from 'axios';

export class AdminService {
    async fetchUsers(userId) {
        let result = await axios.get(`/api/Admin/Get?id=${userId}`);
        return result;
    }

    async banUser(id) {
        let result = await axios.post(`/api/Admin/Ban?id=${id}`);
        return result;
    }

    async unbanUser(id) {
        let result = await axios.post(`/api/Admin/Unban?id=${id}`);
        return result;
    }
}

const adminService = new AdminService();
export default adminService;