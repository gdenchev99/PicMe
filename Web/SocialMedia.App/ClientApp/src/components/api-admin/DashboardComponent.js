import React from 'react';

function DashboardComponent(params) {
    return (
        <React.Fragment>
            <h3 className="offset-4">Admin Dashboard</h3>
            <div className="row">
                <div className="col-12 user-dashboard">
                    <table className="table table-hover">
                        <thead className="bg-primary text-light">
                            <tr>
                                <th scope="col">Id</th>
                                <th scope="col">Username</th>
                                <th scope="col">Full Name</th>
                                <th scope="col">Registered On</th>
                                <th scope="col">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {params.data.map(u => 
                                <tr key={u.id}>
                                <th scope="row">{u.id}</th>
                                <td><img className="user-icon" src={u.profilePictureUrl}/>{u.userName}</td>
                                <td>{u.firstName} {u.lastName}</td>
                                <td>{u.createdOn}</td>
                                <td>{u.lockoutEnd === "0001-01-01T00:00:00+00:00" ? 
                                    <button className="btn bg-danger text-light" onClick={() => params.handleBan(u.id)}>Ban</button> : <button className="btn bg-success text-light" onClick={() => params.handleUnban(u.id)}>Unban
                                    </button>}
                                </td>
                            </tr>)}
                        </tbody>
                    </table>
                </div>
            </div>
        </React.Fragment>
    )
}

export default DashboardComponent