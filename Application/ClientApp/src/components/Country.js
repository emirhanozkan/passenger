import React, { Component } from 'react';
import { Table } from 'reactstrap';
import axios from 'axios';
import config from "../config.json"
import $ from "jquery"
import { Link } from "react-router-dom";

export class Country extends Component {
    static displayName = Country.name;

    constructor(props) {
        super(props);
        this.state = {
           passengers: []
        };
        this.list = this.list.bind(this);
        this.view = this.view.bind(this);
        this.delete = this.delete.bind(this);
    }

    componentDidMount() {
        this.list();
    }

    list = async () => {
        try {
            var response = (await axios({
                method: "GET",
                url: config.api.url + "passenger/country",
                headers: {
                    "Content-Type": "application/json"
                }
            }));
            this.setState({
                passengers: response.data
            })
        } catch (e) {
            var d = e.response.data;
            console.log(d.errors);
            alert(d.title);
        }
    }
    view = async (id) => {
        try {
            var response = (await axios({
                method: "GET",
                url: config.api.url + "passenger/" + id,
                headers: {
                    "Content-Type": "application/json"
                }
            }));
            console.log(response.data);
        } catch (e) {
            var d = e.response.data;
            console.log(d.errors);
            alert(d.title);
        }
    }

    delete = async (id) => {
        if (!window.confirm("Are you sure to delete passenger?")) return;
        try {
            var response = (await axios({
                method: "DELETE",
                url: config.api.url + "passenger/" + id,
                headers: {
                    "Content-Type": "application/json"
                }
            }));
            this.list();
        } catch (e) {
            console.log(e);
            alert(e.response.data.error);
        }
    }

    render() {
        return (
            <div>
                <h3>Passengers - Country</h3>
                <Table striped bordered>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Unique Passenger Id</th>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Gender</th>
                            <th>Document</th>
                            <th>Issue Date</th>
                            <th>Country</th>
                            <th>View</th>
                            <th>Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.passengers.map((x, i) => {
                                return (
                                    <tr key={i}>
                                        <th>{i + 1}</th>
                                        <td>{x.uniquePassengerId}</td>
                                        <td>{x.name}</td>
                                        <td>{x.surname}</td>
                                        <td>{x.gender}</td>
                                        <td>{x.document}</td>
                                        <td>{x.issueDate}</td>
                                        <td>{x.countries[0].country}</td>
                                        <td>
                                            <Link to={`/details/${x.uniquePassengerId}${this.props.location.pathname}` } className="btn btn-primary">View</Link>
                                        </td>
                                        <td>
                                            <button type="button" className="btn btn-danger" onClick={() => this.delete(x.uniquePassengerId) }>Delete</button>
                                        </td>
                                    </tr>
                                )
                            })
                        }
                    </tbody>
                </Table>
            </div>
        );
    }
}
