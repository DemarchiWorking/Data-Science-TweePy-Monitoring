import pytest
import unittest


from PythonDockerFlaskPycharm.app.app import User, Tweet, app

user1 = User(1, "test", 15, 100)
tweet1 = Tweet(1, "test", "pt", "#itau", "en", "#i")


#class TestHome(unittest.TestCase):


def test_content_type(self):
    self.assertIn('text/html', self.response.content_type)


class TestFatorial(unittest.TestCase):

    def test_fatorial(self):
        self.assertEqual(1, 1)

if __name__ == '__main__':
    unittest.main()