import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    constructor(props) {
        super(props);
        this.state =
        {
            searchInput: '',
            responseData: [],
            dataLoaded: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleItemOnClick = this.handleItemOnClick.bind(this);
    }

    static renderSearchResults(movieTitles) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Name</th>
                    </tr>
                </thead>
                <tbody>
                    {movieTitles.map(movie =>
                        <tr key={movie.id}>
                            <td>{movie.name}</td>
                            <td><button onClick={this.handleItemOnClick}>View Details</button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    handleItemOnClick(event) {
        
        console.log(event.target.key);
    }

    handleChange(event) {
      this.setState({ searchInput: event.target.value, responseData: [], dataLoaded: false });
    }

    handleSubmit(event) {
        event.preventDefault();
        
        fetch('api/movies/titles?name=' + this.state.searchInput)
            .then(response => response.json())
            .then(data => {
                this.setState({ searchInput: this.state.searchInput, responseData: data, dataLoaded: true })
            });
    }

    render() {
        let contents = this.state.responseData.length > 0
            ? Home.renderSearchResults(this.state.responseData)
            : <p> <em>Results...</em></p>;
        
    return (
        <form onSubmit={this.handleSubmit}>
            <label>
                Search by Title:
                <input type="text" value={this.state.searchInput} onChange={this.handleChange} />
            </label>
            <input type="submit" value="Submit" />
            {contents}
          </form>
    );
  }
}
