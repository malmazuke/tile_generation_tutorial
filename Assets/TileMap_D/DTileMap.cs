using UnityEngine;
public class DTileMap {

	public enum TYPE {
		BLANK,
		FLOOR,
		WALL,
		ROCK
	}

	int size_x;
	int size_y;

	TYPE[,] map_data;

	public DTileMap(int size_x, int size_y) {
		this.size_x = size_x;
		this.size_y = size_y;

		map_data = new TYPE[this.size_x, this.size_y];

		for (int i = 0; i < 10; i++) {
			int r_sizeX = Random.Range(4, 8);
			int r_sizeY = Random.Range(4, 8);

			MakeRoom (Random.Range(0, size_x - r_sizeX), Random.Range(0, size_y - r_sizeY), r_sizeX, r_sizeY);
		}
	}

	public TYPE GetTileAt(int x, int y) {
		return map_data [x, y];
	}

	void MakeRoom(int left, int top, int width, int height) {

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
					map_data[left + x, top + y] = TYPE.WALL;
				} else {
				map_data[left + x, top + y] = TYPE.FLOOR;
				}
			}
		}
	}
}
