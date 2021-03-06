import authService from './api-authorization/AuthorizeService';
import axios from 'axios';

class Settings {
    refreshToken() {
        axios.interceptors.response.use((response) => {
            return response;
        }, (error) => {
            // Return any error which is not due to authentication back to the calling service
            if (error.response.status !== 401) {
                return new Promise((resolve, reject) => {
                    window.location.href = "/404";
                    reject(error);
                });
            }

            return authService.getAccessToken()
                .then((token) => {
                    const config = error.config;
                    config.headers['Authorization'] = `Bearer ${token}`;

                    return new Promise((resolve, reject) => {
                        axios.request(config).then(response => {
                            resolve(response);
                            axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
                        }).catch((error) => {
                            reject(error);
                        })
                    });
                }).catch((error) => {
                    Promise.reject(error);
                });
        });
    }
}
const settings = new Settings();

export default settings;