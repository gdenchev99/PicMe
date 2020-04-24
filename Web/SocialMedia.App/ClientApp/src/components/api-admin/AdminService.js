import axios from 'axios';

export class AdminService {
    async fetchUsers(userId) {
        let result = await axios.get(`/api/Admin/Get?id=${userId}`);
        return result;
    }

    async banUser(id, adminId) {
        let data = {
            userId: id,
            adminId, adminId
        }

        let result = await axios.post(`/api/Admin/Ban`, data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        return result;
    }

    async unbanUser(id, adminId) {
        let data = {
            userId: id,
            adminId, adminId
        }

        let result = await axios.post(`/api/Admin/Unban`, data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        return result;
    }
}

const adminService = new AdminService();
export default adminService;