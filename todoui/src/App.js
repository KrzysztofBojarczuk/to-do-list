import './App.css';
import {
    Button,
    Navbar,
    Container,
    Nav,
    InputGroup,
    FormControl,
    Row,
    Col,
    Form,
    ListGroup
} from 'react-bootstrap';
import {useState, useEffect} from 'react';
import axios from 'axios';


function App() {
    const [storeItem,
        setStoreItem] = useState([
        {
            todoId: 0,
            comment: ''
        }
    ]);
    const [postData,
        setPostData] = useState({todoId: 0, comment: ''});

    const handleChange = (event) => {
        setPostData({
            ...postData,
            comment: event.target.value
        })
    }
    useEffect(() => {
        const url = 'http://localhost:7120/api/Todo/Get'
        axios
            .get(url)
            .then(res => {
                console.log(res.data);
                setStoreItem(res.data);
            })
    }, [])
    const handleSubmit = (e) => {
        const url =  'http://localhost:7120/api/Todo/Post'
        e.preventDefault()
        axios
            .post(url, postData)
            .then(res => {
                setStoreItem([
                    ...storeItem,
                    res.data
                ])
            })
            .catch(err => {
                console.log(err);
            })
    }

    const handleDelete = (id) => {
        const url = `http://localhost:7120/api/Todo/Delete/${id}`
        axios
            .delete(url,)
            .then(res => {

                setStoreItem(storeItem.filter((todo) => todo.todoId !== id))

            })
            .catch(err => {
                console.log(err);
            })
    }
    return (
        <div className='body'>

            <Container>

                <Row className="justify-content-md-center">
                    <Col className="mt-5" lg={4}>
                    <InputGroup className="mb-3" aria-describedby="basic-addon2">

                        <Form.Control type="text" value={storeItem.comment} onChange={handleChange}/>

                        <Button
                            className="ms-3"
                            onClick={handleSubmit}
                            variant="secondary"
                            id="button-addon2">Submit</Button>

                    </InputGroup>
                            </Col>
                </Row>

                <Row className="justify-content-md-center">
                    <div className='col-4'>

                        <ListGroup>

                            {storeItem.map((item) => (

                                <ListGroup.Item className="mt-2 text-break text-center" key={item.todoId}>{item.comment}

                                    <div className="d-grid justify-content-end">
                                        <Button
                                            className=""
                                            onClick={() => handleDelete(item.todoId)}
                                            variant="secondary">Delete</Button>
                                    </div>

                                </ListGroup.Item>

                            ))}
                        </ListGroup>

                    </div>
                </Row>

            </Container>

        </div>
    );
}
export default App;