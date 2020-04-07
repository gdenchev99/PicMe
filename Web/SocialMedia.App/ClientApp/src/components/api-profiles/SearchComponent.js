import React from 'react';
import { Link } from 'react-router-dom';

function SearchComponent(params) {
    return (
        <React.Fragment>
            <div className="searchBar">
                <input type="text" value={params.query} onChange={params.handleChange} placeholder="Search..." />
                <i className="fas fa-search search-icon" />
            </div>
            <div id="search-results" className="dropdown-menu p-0">
                <ul className="m-0 p-0">
                    {params.results.length > 0 ? params.results.map(r =>
                        <Link key={r.userName} to={`/user/${r.userName}`}>
                            <li className="dropdown-item">
                                <span><img src={r.profilePictureUrl} className="search-picture"/></span> 
                                <span>{r.userName}</span>
                                <small className="search-fullname">{r.fullName}</small>
                            </li>
                        </Link>
                    ) : <p className="text-center">No Results</p>}
                </ul>
            </div>
        </React.Fragment>
    );
}

export default SearchComponent