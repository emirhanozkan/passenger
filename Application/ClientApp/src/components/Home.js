import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input, Badge } from 'reactstrap';
import axios from 'axios';
import config from "../config.json"
import $ from "jquery"

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            count: 0,
            documentTypes: [],
            genderTypes: [],
            statusTypes: [],
            countryTypes: []
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.createPassenger = this.createPassenger.bind(this);
        this.countPassengers = this.countPassengers.bind(this);
    }

    componentDidMount() {
        axios({
            method: "POST",
            url: config.api.url + "misc/enums",
            headers: {
                "Content-Type": "application/json"
            },
            data: {
                "names": [
                    "DocumentType",
                    "GenderType",
                    "StatusType",
                    "CountryType"
                ]
            }
        }).then(x => {
            this.setState({
                documentTypes: x.data.filter(x => x.key == "DocumentType")[0].value,
                genderTypes: x.data.filter(x => x.key == "GenderType")[0].value,
                statusTypes: x.data.filter(x => x.key == "StatusType")[0].value,
                countryTypes: x.data.filter(x => x.key == "CountryType")[0].value
            });
            this.countPassengers();
        })
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        var form = $("#form").serializeArray();
        var data = (() => {
            var result = {};
            for (var i = 0; i < form.length; i++) {
                var el = form[i];
                var value = el.value;
                var isNull = (el.value === undefined || el.value == "");
                switch (el.name) {
                    case "documentType":
                        value = parseInt(el.value);
                        break;
                    case "genderType":
                        value = parseInt(el.value);
                        break;
                    case "statusType":
                        value = !isNull ? parseInt(el.value) : null;
                        break;
                    case "countryType":
                        value = !isNull ? parseInt(el.value) : null;
                        break;
                    default:
                        break;
                }
                result[el.name] = value;
            }
            return result;
        })();   
        if (this.createPassenger(data))
        {
            $("#form").find("input, select, textarea").val("");
        };
        setTimeout(() => {
            this.countPassengers();
        }, 500);
    };

    createPassenger = async (data) => {
        try {
            var response = (await axios({
                method: "POST",
                url: config.api.url + "passenger",
                headers: {
                    "Content-Type": "application/json"
                },
                data: data
            }));
            return true;
        } catch (e) {
            console.log(e);
            alert(e.response.data.error);
            return false;
        }
    }

    countPassengers = async () => {
        try {
            var response = (await axios({
                method: "GET",
                url: config.api.url + "passenger/count",
                headers: {
                    "Content-Type": "application/json"
                }
            }));
            this.setState({
                count: response.data
            })
        } catch (e) {
            console.log(e);
        }
    }

    render() {
        return (
            <div>
                <h6>Passenger Count: <Badge color="primary">{this.state.count}</Badge></h6>
                <Form onSubmit={this.handleSubmit} id="form">
                    <FormGroup>
                        <Label for="name">Name</Label>
                        <Input type="text" name="name" id="name" required />
                    </FormGroup>
                    <FormGroup>
                        <Label for="surname">Surname</Label>
                        <Input type="text" name="surname" id="surname" required />
                    </FormGroup>
                    <FormGroup>
                        <Label for="genderType">Gender</Label>
                        <Input type="select" name="genderType" id="genderType" required>
                            <option value="">Please select</option>
                            {
                                this.state.genderTypes.map((x, i) => {
                                    return (<option key={i} value={x.id}>{x.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup>
                        <Label for="documentType">Document Type</Label>
                        <Input type="select" name="documentType" id="documentType" required>
                            <option value="">Please select</option>
                            {
                                this.state.documentTypes.map((x, i) => {
                                    return (<option key={i} value={x.id}>{x.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup>
                        <Label for="documentNumber">Document Number</Label>
                        <Input type="text" name="documentNo" id="documentNumber" required />
                    </FormGroup>
                    <FormGroup>
                        <Label for="issueDate">Issue Date</Label>
                        <Input
                            type="date"
                            name="issueDate"
                            id="issueDate"
                            placeholder="date placeholder"
                            required
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for="statusType">Status Type</Label>
                        <Input type="select" name="statusType" id="statusType">
                            <option value="">Please select</option>
                            {
                                this.state.statusTypes.map((x, i) => {
                                    return (<option key={i} value={x.id}>{x.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup>
                        <Label for="countryType">Country Type</Label>
                        <Input type="select" name="countryType" id="countryType">
                            <option value="">Please select</option>
                            {
                                this.state.countryTypes.map((x, i) => {
                                    return (<option key={i} value={x.id}>{x.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button type="submit" className="btn btn-primary">Save</Button>
                </Form>
            </div>
        );
    }
}
