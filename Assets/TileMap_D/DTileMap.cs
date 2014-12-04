
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

//	public DTileMap() {
//		this(20, 20);
//	}

	public DTileMap(int size_x, int size_y) {
		this.size_x = size_x;
		this.size_y = size_y;

		map_data = new TYPE[this.size_x, this.size_y];

		MakeRoom (3, 3, 5, 5);
	}

	public TYPE GetTileAt(int x, int y) {
		return map_data [x, y];
	}

	void MakeRoom(int left, int top, int width, int height) {

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				map_data[left + x, top + y] = TYPE.FLOOR;
			}
		}
	}
}
