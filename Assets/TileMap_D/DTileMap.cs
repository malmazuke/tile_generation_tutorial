using UnityEngine;
using System.Collections.Generic;

public class DTileMap {

	protected class DRoom {
		public int left;
		public int top;
		public int width;
		public int height;
		
		public int right {
			get { return this.left + this.width - 1; }
		}
		
		public int bottom {
			get { return this.top + this.height - 1; }
		}

		public int centerX {
			get { return left + width / 2; }
		}

		public int centerY {
			get { return top + height / 2; }
		}
		
		public bool CollidesWith(DRoom other) {
			if (this.left   > other.right  - 1) return false;
			if (this.top    > other.bottom - 1) return false;
			if (this.right  < other.left   + 1) return false;
			if (this.bottom < other.top    + 1) return false;
			
			return true;
		}
	}
	
	public enum TYPE {
		BLANK,
		FLOOR,
		WALL,
		ROCK
	}

	int _sizeX;
	int _sizeY;
	
	TYPE[,] _mapData;
	
	List<DRoom> _rooms;

	public DTileMap(int sizeX, int sizeY) {
		this._sizeX = sizeX;
		this._sizeY = sizeY;

		_mapData = new TYPE[this._sizeX, this._sizeY];
		
		for (int x = 0; x < this._sizeX; x++) {
			for (int y = 0; y < this._sizeY; y++) {
				_mapData[x,y] = TYPE.ROCK;
			}
		}

		_rooms = new List<DRoom>();

		int maxFails = 10;
		while (_rooms.Count < 10 && maxFails != 0) {
			int rSizeX = Random.Range(4, 14);
			int rSizeY = Random.Range(4, 10);

			DRoom r = new DRoom();
			r.left = Random.Range(0, this._sizeX - rSizeX);
			r.top = Random.Range(0, this._sizeY - rSizeY);
			r.width = rSizeX;
			r.height = rSizeY;
			
			if (!RoomCollides(r)) {
				_rooms.Add(r);
			} else {
				maxFails--;
			}
		}
		
		foreach(DRoom r in _rooms) {
			MakeRoom(r);
		}

		MakeCorridor (_rooms [0], _rooms [1]);
	}

	bool RoomCollides(DRoom r) {
		foreach(DRoom r2 in _rooms) {
			if (r.CollidesWith(r2)){
				return true;
			}
		}
		
		return false;
	}
	
	public TYPE GetTileAt(int x, int y) {
		return _mapData [x, y];
	}

	void MakeRoom(DRoom room) {
		for (int x = 0; x < room.width; x++) {
			for (int y = 0; y < room.height; y++) {
				if (x == 0 || x == room.width - 1 || y == 0 || y == room.height - 1) {
					_mapData[room.left + x, room.top + y] = TYPE.WALL;
				} else {
					_mapData[room.left + x, room.top + y] = TYPE.FLOOR;
				}
			}
		}
	}

	void MakeCorridor(DRoom r1, DRoom r2) {
		int x = r1.centerX;
		int y = r1.centerY;

		while (x != r2.centerX) {
			_mapData[x, y] = TYPE.FLOOR;
			x += x < r2.centerX ? 1 : -1;
		}
		while (y != r2.centerY) {
			_mapData[x, y] = TYPE.FLOOR;
			y += y < r2.centerY ? 1 : -1;
		}
	}
}
