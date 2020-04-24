import React, {Component} from 'react';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';
import { Redirect } from 'react-router-dom';
import DashboardComponent from './DashboardComponent';
import adminService from './AdminService';

export class Dashboard extends Component {
    constructor(props) {
        super(props);

        this.state = {
            status: null,
            userId: null,
            data: []
        }

        this.banUser = this.banUser.bind(this);
        this.unbanUser = this.unbanUser.bind(this);
    }

    async componentDidMount() {
        const user = await authService.getUser();
        const userId = user.sub;
        this.setState({userId: userId});

        await this.handleData();
        console.log(this.state.data)
        
    }

    handleData = async() => {
        await adminService.fetchUsers(this.state.userId)
            .then(response => {
                if (response == undefined) {
                    this.setState({status: "failed"})
                } else {
                    this.setState({data: response.data})
                }
            })
            .catch(error => {
                this.setState({status: "failed"});
            })
    }

    banUser = async(id) => {
        let result = await adminService.banUser(id, this.state.userId);
        
        let array = this.state.data;
        array.find(x => x.id == id).lockoutEnd = result.data; // Update the value inside the array without re-fetching so the button can update.

        this.setState({data: array})
    }

    unbanUser = async(id) => {
        await adminService.unbanUser(id, this.state.userId);
        
        let array = this.state.data;
        array.find(x => x.id == id).lockoutEnd = "0001-01-01T00:00:00+00:00"; // Update the value inside the array without re-fetching so the button can update.

        this.setState({data: array})
    }

    render() {
        if(this.state.status) {
            return <Redirect to="/404"/>
        }

        return(
            <DashboardComponent data={this.state.data} 
            handleBan={this.banUser}
            handleUnban={this.unbanUser}/>
        );
    }
}