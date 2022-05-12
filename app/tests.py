import pytest
import unittest

from app import User, Tweet, app

class TestHome(unittest.TestCase):

    def setUp(self):
        self.client = app.test_client()

    def test_get_raiz(self):
        response = self.client.get('/')
        self.assertEqual(200, response.status_code)

    def test_content_type(self):
        test = app.test_client()
        response = test.get('/')
        self.assertIn('text/html', response.content_type)


    def test_get_register_tweet(self):
        response = self.client.get('/cadastrar-tweet')
        self.assertEqual(200, response.status_code)

    def test_form(self):
        response = self.client.get('/cadastrar-tweet')
        response_str = response.data.decode('utf-8')
        self.assertIn('<form', str(response_str))
        self.assertIn('<input', str(response_str))



if __name__ == '__main__':
    unittest.main()

